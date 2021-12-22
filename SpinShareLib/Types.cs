using System;

namespace SpinShareLib 
{
    namespace Types 
    {
        public class Content
        {
            public int version;
            public int status;
        }

        public class Content<T> : Content
        {
            public T data;
        }

        public class Promo
        {
            public int id;
            public string
                title,
                type,
                textColor,
                color,
                image_path;
            public bool isVisible;
            public Button button;
            public class Button
            {
                public int type;
                public string data;
            }
        }

        public class Song
        {
            public int id;
            public int? 
                easyDifficulty,
                normalDifficulty,
                hardDifficulty,
                expertDifficulty,
                XDDifficulty;
            public bool
                hasEasyDifficulty,
                hasNormalDifficulty,
                hasHardDifficulty,
                hasExpertDifficulty,
                hasXDDifficulty;
            public string 
                title,
                subtitle,
                artist,
                Songer,
                updateHash,
                cover,
                zip;
        }
        public class SongDetail : Song
        {
            public int?
                uploader,
                views,
                downloads,
                publicationStatus;
            public string description;
            public string[] tags;
            public Date uploadDate;
            public Paths paths;
            public class Paths
            {
                public string
                    ogg,
                    cover,
                    zip;
            }
        }
        public class Reviews
        {
            public bool average;
            public Review[] reviews;
            public class Review
            {
                public int id;
                public SongDetail song;
                public User user;
                public bool recommended;
                public string comment;
                public Date reviewDate;
            }
        }
        public class SpinPlays
        {
            public Spinplay[] spinPlays;
            public class Spinplay
            {
                public int id;
                public User user;
                public bool? isActive;
                public Date submitDate;
                public string
                    videoUrl,
                    videoThumbnail;

            }
        }
        public class User
        {
            public int id;
            public bool?
                isVerified,
                isPatreon;
            public string
                username,
                avatar;
        }
        public class UserDetail : User
        {
            public string pronouns;
            public int?
                songs,
                playlists,
                reviews,
                spinplay;
            public Card[] cards;
            public class Card
            {
                public int id;
                public string
                    icon,
                    title,
                    description;
                public Date givenDate;
                public int?
                    songs,
                    playlists,
                    reviews,
                    spinplay;
            }
        }

        public class Search
        {
            public Song[] songs;
            public User[] users;
        }

        public class Date
        {
            public DateTime date;
            public string
                timezone;
            public int timezone_type;
        }
    }
}