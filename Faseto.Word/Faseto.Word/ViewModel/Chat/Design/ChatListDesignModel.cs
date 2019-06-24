﻿using System.Collections.Generic;

namespace Fasetto.Word
{
    public class ChatListDesignModel : ChatListViewModel
    {
        #region Singleton
        public static ChatListDesignModel Instance => new ChatListDesignModel();
        #endregion

        #region Constructor
        public ChatListDesignModel()
        {
            Items = new List<ChatListItemViewModel>
            {
                new ChatListItemViewModel
                {
                    Name = "Luke",
                    Initials = "LM",
                    Message = "This chat app is awesome! I bet it will be fast too.",
                    ProfilePictureRGB = "3099c5"
                },
                new ChatListItemViewModel
                {
                    Name = "Jesse",
                    Initials = "JA",
                    Message = "Hey dude, here are the new icons",
                    ProfilePictureRGB = "fe4503"
                },
                new ChatListItemViewModel
                {
                    Name = "Parnell",
                    Initials = "PL",
                    Message = "The new server is up, got 192.168.1.1",
                    ProfilePictureRGB = "00d405"
                },
            };
        }
        #endregion
    }
}
