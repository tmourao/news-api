﻿using News.Core.Web.Models.Hacker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.Services.Hacker
{
    public class HackerNewsService : IHackerNewsService
    {
        #region Private Fields

        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<HackerStory>> GetBestStories(int size)
        {
            var bestStories = new List<HackerStory>();

            var data = await _httpClient.GetAsync("/v0/beststories.json");

            // possible exception needs to be handled
            data.EnsureSuccessStatusCode();

            var content = await data.Content.ReadAsStringAsync();
            var bestStoryIds = JsonConvert.DeserializeObject<IEnumerable<int>>(content);

            var detailTasks = bestStoryIds.Select(id => _httpClient.GetAsync($"/v0/item/{id}.json"));
            var detailData = await Task.WhenAll(detailTasks);

            foreach(var response in detailData)
            {
                // possible exception needs to be handled
                response.EnsureSuccessStatusCode();

                var storyDetail = await response.Content.ReadAsStringAsync();
                var bestStory = JsonConvert.DeserializeObject<HackerStory>(storyDetail);

                bestStories.Add(bestStory);
            }

            return bestStories
                .OrderByDescending(x => x.Score)
                .Take(size);
        }

        #endregion
    }
}