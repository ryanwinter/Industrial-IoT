// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace IIoTPlatform_E2E_Tests.Orchestrated {
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using TestExtensions;
    using Xunit;
    using Xunit.Abstractions;

    [TestCaseOrderer(TestCaseOrderer.FullName, TestConstants.TestAssemblyName)]
    [Collection("IIoT Multiple Nodes Test Collection")]
    [Trait(TestConstants.TraitConstants.PublisherModeTraitName, TestConstants.TraitConstants.PublisherModeOrchestratedTraitValue)]
    public class C_DiscoverEndpointsTestTheory {
        private readonly ITestOutputHelper _output;
        private readonly IIoTMultipleNodesTestContext _context;

        public C_DiscoverEndpointsTestTheory(IIoTMultipleNodesTestContext context, ITestOutputHelper output) {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.OutputHelper = _output;
        }

        [Fact, PriorityOrder(0)]
        public void D1_Discover_All_OPC_UA_Endpoints() {
            // Switch to Orchestrated mode
            TestHelper.SwitchToOrchestratedModeAsync(_context).GetAwaiter().GetResult();

            // Add servers
            var endpointUrls = AddServers(10);
            
            // Discover all servers
            var cts = new CancellationTokenSource(TestConstants.MaxTestTimeoutMilliseconds);
            dynamic result = TestHelper.WaitForDiscoveryToBeCompletedAsync(_context, cts.Token, requestedEndpointUrls: endpointUrls).GetAwaiter().GetResult();

            // Validate that all servers are discovered
            var applicationIds = new List<string>(endpointUrls.Count);
            Assert.Equal(endpointUrls.Count, result.items.Count);
            for (int i = 0; i < result.items.Count; i++) {
                Assert.Equal("Server", result.items[i].applicationType);
                Assert.True(endpointUrls.Contains(result.items[i].discoveryUrls[0].TrimEnd('/')));
                applicationIds.Add(result.items[i].applicationId);
            }

            // Remove all servers
            RemoveAllApplications(applicationIds);
        }

        [Fact, PriorityOrder(1)]
        public void D2_Discover_OPC_UA_Endpoints_IpAddress() {
            // Switch to Orchestrated mode
            TestHelper.SwitchToOrchestratedModeAsync(_context).GetAwaiter().GetResult();

            var cts = new CancellationTokenSource(TestConstants.MaxTestTimeoutMilliseconds);

            // Add one server
            var endpointUrls = AddServers(1);
            
            
            dynamic result = TestHelper.WaitForDiscoveryToBeCompletedAsync(_context, cts.Token, requestedEndpointUrls: endpointUrls).GetAwaiter().GetResult();
            var a = result.items[0].applicationId;
            var ipAddress = result.items[0].hostAddresses[0];
            
            var accessToken = TestHelper.GetTokenAsync(_context, cts.Token).GetAwaiter().GetResult();
            var client = new RestClient(_context.IIoTPlatformConfigHubConfig.BaseUrl) { Timeout = TestConstants.DefaultTimeoutInMilliseconds };

            var adr = ipAddress.Replace(":50000","") + "/16";
            var body = new {
                configuration = new {
                    addressRangesToScan = adr
                }
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader(TestConstants.HttpHeaderNames.Authorization, accessToken);
            request.Resource = TestConstants.APIRoutes.RegistryApplications + "/discover";
            request.AddJsonBody(JsonConvert.SerializeObject(body));
            var response = client.ExecuteAsync(request, cts.Token).GetAwaiter().GetResult();
            Assert.True(response.IsSuccessful);
            
            result = TestHelper.WaitForEndpointDiscoveryToBeCompleted(_context, cts.Token, requestedEndpointUrls: endpointUrls).GetAwaiter().GetResult();
            Assert.Single(result);

            RemoveAllApplications(new List<string> { a });
        }

        private List<string> AddServers(int maxNumberOfServers) {
            var cts = new CancellationTokenSource(TestConstants.MaxTestTimeoutMilliseconds);
            var accessToken = TestHelper.GetTokenAsync(_context, cts.Token).GetAwaiter().GetResult();
            var simulatedOpcServer = TestHelper.GetSimulatedPublishedNodesConfigurationAsync(_context, cts.Token).GetAwaiter().GetResult();

            var client = new RestClient(_context.IIoTPlatformConfigHubConfig.BaseUrl) { Timeout = TestConstants.DefaultTimeoutInMilliseconds };
            var endpointUrls = simulatedOpcServer.Values.Select(s => s.EndpointUrl).ToList();

            if (endpointUrls.Count < maxNumberOfServers) {
                maxNumberOfServers = endpointUrls.Count;
            }

            for (int i = 0; i < maxNumberOfServers; i++) {
                var body = new {
                    discoveryUrl = endpointUrls[i]
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader(TestConstants.HttpHeaderNames.Authorization, accessToken);
                request.Resource = TestConstants.APIRoutes.RegistryApplications;

                request.AddJsonBody(JsonConvert.SerializeObject(body));

                var response = client.ExecuteAsync(request, cts.Token).GetAwaiter().GetResult();
                Assert.NotNull(response);
            }

            return endpointUrls.Take(maxNumberOfServers).ToList();
        }
        private void RemoveApplication(string applicationId) {
            var cts = new CancellationTokenSource(TestConstants.MaxTestTimeoutMilliseconds);
            var accessToken = TestHelper.GetTokenAsync(_context, cts.Token).GetAwaiter().GetResult();
            var client = new RestClient(_context.IIoTPlatformConfigHubConfig.BaseUrl) { Timeout = TestConstants.DefaultTimeoutInMilliseconds };

            var request = new RestRequest(Method.DELETE);
            request.AddHeader(TestConstants.HttpHeaderNames.Authorization, accessToken);
            request.Resource = $"{TestConstants.APIRoutes.RegistryApplications}/{applicationId}";
            var response = client.ExecuteAsync(request, cts.Token).GetAwaiter().GetResult();
            Assert.True(response.IsSuccessful);
        }

        private void RemoveAllApplications(List<string> applicationIds) {
            var cts = new CancellationTokenSource(TestConstants.MaxTestTimeoutMilliseconds);

            var accessToken = TestHelper.GetTokenAsync(_context, cts.Token).GetAwaiter().GetResult();
            var client = new RestClient(_context.IIoTPlatformConfigHubConfig.BaseUrl) { Timeout = TestConstants.DefaultTimeoutInMilliseconds };

            var request = new RestRequest(Method.DELETE);
            request.AddHeader(TestConstants.HttpHeaderNames.Authorization, accessToken);
            request.Resource = TestConstants.APIRoutes.RegistryApplications;

            var response = client.ExecuteAsync(request, cts.Token).GetAwaiter().GetResult();
            Assert.True(response.IsSuccessful);

            // Check that all servers are removed
            var c = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            Assert.Throws<OperationCanceledException>(() => TestHelper.WaitForDiscoveryToBeCompletedAsync(_context, c.Token).GetAwaiter().GetResult());
        }
    }
}
