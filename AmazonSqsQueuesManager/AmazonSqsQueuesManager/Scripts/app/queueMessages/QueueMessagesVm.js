define(['ko'],function(ko) {
    'use strict';

    return {
        messagesCount: ko.observable(),
        messageFromQueue: ko.observable(),
        messageToQueue: ko.observable()
    };
});