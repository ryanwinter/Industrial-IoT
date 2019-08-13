// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.IIoT.Opc.Registry.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for SupervisorLogLevel.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SupervisorLogLevel
    {
        [EnumMember(Value = "Error")]
        Error,
        [EnumMember(Value = "Information")]
        Information,
        [EnumMember(Value = "Debug")]
        Debug,
        [EnumMember(Value = "Verbose")]
        Verbose
    }
    internal static class SupervisorLogLevelEnumExtension
    {
        internal static string ToSerializedValue(this SupervisorLogLevel? value)
        {
            return value == null ? null : ((SupervisorLogLevel)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this SupervisorLogLevel value)
        {
            switch( value )
            {
                case SupervisorLogLevel.Error:
                    return "Error";
                case SupervisorLogLevel.Information:
                    return "Information";
                case SupervisorLogLevel.Debug:
                    return "Debug";
                case SupervisorLogLevel.Verbose:
                    return "Verbose";
            }
            return null;
        }

        internal static SupervisorLogLevel? ParseSupervisorLogLevel(this string value)
        {
            switch( value )
            {
                case "Error":
                    return SupervisorLogLevel.Error;
                case "Information":
                    return SupervisorLogLevel.Information;
                case "Debug":
                    return SupervisorLogLevel.Debug;
                case "Verbose":
                    return SupervisorLogLevel.Verbose;
            }
            return null;
        }
    }
}