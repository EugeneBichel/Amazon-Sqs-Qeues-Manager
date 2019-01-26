define(['jquery', 'QueueMessagesSettings'],
    function ($, queueMessagesSettings) {
    'use strict';

    function QueueMessagesService() {
        var self = this;
        self.urlMessagesCount = queueMessagesSettings.urlMessagesCount;
        self.urlDequeue = queueMessagesSettings.urlDequeue;
        self.urlEnqueue = queueMessagesSettings.urlEnqueue;
        self.urlPurge = queueMessagesSettings.urlPurge;

        self.getMessagesCount = function() {
            return $.getJSON(self.urlMessagesCount);
        };

        self.dequeueMessage = function() {
            return $.getJSON(self.urlDequeue);
        };

        self.engueueMessage = function(json) {
            return $.ajax({
                method: "POST",
                url: self.urlEnqueue,
                data: json,
                dataType: 'json',
                contentType: "application/json; charset=utf-8"
            });
        };

        self.purgeQueue = function() {
            return $.getJSON(self.urlPurge);
        };
    };

    return new QueueMessagesService();
});