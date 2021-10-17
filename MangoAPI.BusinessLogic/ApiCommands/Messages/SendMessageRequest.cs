﻿using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace MangoAPI.BusinessLogic.ApiCommands.Messages
{
    public record SendMessageRequest
    {
        [JsonConstructor]
        public SendMessageRequest(string messageText,
            Guid chatId,
            bool isEncrypted,
            string attachmentUrl,
            string inReplayToAuthor,
            string inReplayToText)
        {
            MessageText = messageText;
            ChatId = chatId;
            IsEncrypted = isEncrypted;
            AttachmentUrl = attachmentUrl;
            InReplayToAuthor = inReplayToAuthor;
            InReplayToText = inReplayToText;
        }

        [DefaultValue("hello world")]
        public string MessageText { get; }

        [DefaultValue("a8747c37-c5ef-4a87-943c-3ee3ae0a2871")]
        public Guid ChatId { get; }

        [DefaultValue(false)]
        public bool IsEncrypted { get; }

        [DefaultValue("https://localhost:5001/Uploads/khachatur_picture.jpg")]
        public string AttachmentUrl { get; }

        [DefaultValue("John Doe")]
        public string InReplayToAuthor { get; set; }

        [DefaultValue("Hello world!")]
        public string InReplayToText { get; set; }
    }
}