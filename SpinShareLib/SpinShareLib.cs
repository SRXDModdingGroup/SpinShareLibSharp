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
        async public Task<Content<SongDetail[]>> getTournamentMapPool()
        {
            return await this.getApiResultAsType<Content<SongDetail[]>>($"{this.apiBase}tournament/mappool");
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
        async public Task<Content<Search>> searchAll()
        {
            return await this.getApiResultAsType<Content<Search>>($"{this.apiBase}searchAll");
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
}
