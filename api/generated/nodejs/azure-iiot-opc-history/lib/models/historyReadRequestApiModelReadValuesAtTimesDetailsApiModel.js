/*
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

'use strict';

/**
 * Request node history read
 *
 */
class HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel {
  /**
   * Create a HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel.
   * @property {string} [nodeId] Node to read from (mandatory)
   * @property {array} [browsePath] An optional path from NodeId instance to
   * the actual node.
   * @property {object} [details] The HistoryReadDetailsType extension object
   * encoded in json and containing the tunneled
   * Historian reader request.
   * @property {array} [details.reqTimes] Requested datums
   * @property {boolean} [details.useSimpleBounds] Whether to use simple bounds
   * @property {string} [indexRange] Index range to read, e.g. 1:2,0:1 for 2
   * slices
   * out of a matrix or 0:1 for the first item in
   * an array, string or bytestring.
   * See 7.22 of part 4: NumericRange.
   * @property {object} [header] Optional request header
   * @property {object} [header.elevation] Optional User elevation
   * @property {string} [header.elevation.type] Type of credential. Possible
   * values include: 'None', 'UserName', 'X509Certificate', 'JwtToken'
   * @property {object} [header.elevation.value] Value to pass to server
   * @property {array} [header.locales] Optional list of locales in preference
   * order.
   * @property {object} [header.diagnostics] Optional diagnostics configuration
   * @property {string} [header.diagnostics.level] Requested level of response
   * diagnostics.
   * (default: Status). Possible values include: 'None', 'Status',
   * 'Operations', 'Diagnostics', 'Verbose'
   * @property {string} [header.diagnostics.auditId] Client audit log entry.
   * (default: client generated)
   * @property {date} [header.diagnostics.timeStamp] Timestamp of request.
   * (default: client generated)
   */
  constructor() {
  }

  /**
   * Defines the metadata of HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel
   *
   * @returns {object} metadata of HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel
   *
   */
  mapper() {
    return {
      required: false,
      serializedName: 'HistoryReadRequestApiModel_ReadValuesAtTimesDetailsApiModel_',
      type: {
        name: 'Composite',
        className: 'HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel',
        modelProperties: {
          nodeId: {
            required: false,
            serializedName: 'nodeId',
            type: {
              name: 'String'
            }
          },
          browsePath: {
            required: false,
            serializedName: 'browsePath',
            type: {
              name: 'Sequence',
              element: {
                  required: false,
                  serializedName: 'StringElementType',
                  type: {
                    name: 'String'
                  }
              }
            }
          },
          details: {
            required: false,
            serializedName: 'details',
            type: {
              name: 'Composite',
              className: 'ReadValuesAtTimesDetailsApiModel'
            }
          },
          indexRange: {
            required: false,
            serializedName: 'indexRange',
            type: {
              name: 'String'
            }
          },
          header: {
            required: false,
            serializedName: 'header',
            type: {
              name: 'Composite',
              className: 'RequestHeaderApiModel'
            }
          }
        }
      }
    };
  }
}

module.exports = HistoryReadRequestApiModelReadValuesAtTimesDetailsApiModel;