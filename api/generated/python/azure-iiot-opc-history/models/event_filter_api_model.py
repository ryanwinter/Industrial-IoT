# coding=utf-8
# --------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See License.txt in the project root for
# license information.
#
# Code generated by Microsoft (R) AutoRest Code Generator 2.3.33.0
# Changes may cause incorrect behavior and will be lost if the code is
# regenerated.
# --------------------------------------------------------------------------

from msrest.serialization import Model


class EventFilterApiModel(Model):
    """Event filter.

    :param select_clauses: Select statements
    :type select_clauses:
     list[~azure-iiot-opc-history.models.SimpleAttributeOperandApiModel]
    :param where_clause:
    :type where_clause: ~azure-iiot-opc-history.models.ContentFilterApiModel
    """

    _attribute_map = {
        'select_clauses': {'key': 'selectClauses', 'type': '[SimpleAttributeOperandApiModel]'},
        'where_clause': {'key': 'whereClause', 'type': 'ContentFilterApiModel'},
    }

    def __init__(self, select_clauses=None, where_clause=None):
        super(EventFilterApiModel, self).__init__()
        self.select_clauses = select_clauses
        self.where_clause = where_clause