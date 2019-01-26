define(['jquery'], function($) {
    'use strict';

    return {
        urlMessagesCount: '/api/queue/count',
        btnGetCount: $("#btnGetCount"),

        urlDequeue: '/api/queue/dequeue',
        btnDequeue: $("#btnDequeue"),

        urlEnqueue: '/api/queue/enqueue',
        btnEnqueue: $("#btnEnqueue"),

        urlPurge: '/api/queue/purge',
        btnPurgeQueue: $("#btnPurgeQueue"),

        containerId: "queue-items-container"
    };
});