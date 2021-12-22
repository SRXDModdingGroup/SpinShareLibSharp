using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpinShareLib.Types;

namespace SpinShareLib
{
    public class SSAPI
    {
        public string apiBase { get; private set; }
        public int supportedVersion { get; private set; }

        public HttpClient client { get; private set; }

        public SSAPI() : this(new HttpClient()) { }
        public SSAPI(HttpClient client)
        {
            this.apiBase = "https://spinsha.re/api/";
            this.supportedVersion = 1;
            this.client = client;
        }
        
        async public Task<Content> ping()
        {
            return await this.getApiResultAsType<Content>($"{this.apiBase}ping");
        }

        async public Task<Content<Promo[]>> getPromos()
        {
            return await this.getApiResultAsType<Content<Promo[]>>($"{this.apiBase}promos");
        }

        async public Task<Content<Song[]>> getNewSongs(int _offset)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}songs/new/{_offset}");
        }
        async public Task<Content<Song[]>> getHotThisWeekSongs(int _offset)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}songs/hotThisWeek/{_offset}");
        }

        async public Task<Content<SongDetail>> getSongDetail(string _songId)
        {
            return await this.getApiResultAsType<Content<SongDetail>>($"{this.apiBase}song/{_songId}");
        }

        async public Task<Content<Reviews>> getSongDetailReviews(string _songId)
        {
            return await this.getApiResultAsType<Content<Reviews>>($"{this.apiBase}song/{_songId}/reviews");
        }

        async public Task<Content<SpinPlays>> getSongDetailSpinPlays(string _songId)
        {
            return await this.getApiResultAsType<Content<SpinPlays>>($"{this.apiBase}song/{_songId}/spinplays");
        }
        async public Task<Content<UserDetail>> getUserDetail(string _userId)
        {
            return await this.getApiResultAsType<Content<UserDetail>>($"{this.apiBase}user/{_userId}");
        }
        async public Task<Content<Song[]>> getUserCharts(string _userId)
        {
            return await this.getApiResultAsType<Content<Song[]>>($"{this.apiBase}user/{_userId}/charts");
        }
        async public Task<Content<Reviews.Review[]>> getUserReviews(string _userId)
        {
            return await this.getApiResultAsType<Content<Reviews.Review[]>>($"{this.apiBase}user/{_userId}/reviews");
        }
        async public Task<Content<SpinPlays.Spinplay[]>> getUserSpinPlays(string _userId)
        {
            return await this.getApiResultAsType<Content<SpinPlays.Spinplay[]>>($"{this.apiBase}user/{_userId}/spinplays");
        }

        async public Task<Content<Search>> search(string _query)
        {
            return await this.getApiResultAsType<Content<Search>>($"{this.apiBase}search/{_query}");
        }

        async private Task<T> getApiResultAsType<T>(string apiPath)
        {
            var resp = await client.GetAsync(apiPath);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }
    }

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
            public int? 
                id,
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
