﻿using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWeb.AppCode
{
    //https://www.cnblogs.com/CoderAyu/p/9601991.html
    public class EsClientProvider : IEsClientProvider
    {
        private readonly IConfiguration _configuration;
        private ElasticClient _client;
        public EsClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ElasticClient GetClient()
        {
            if (_client != null)
                return _client;

            InitClient();
            return _client;
        }

        private void InitClient()
        {
            var node = new Uri(_configuration["EsUrl"]);
            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex("demo"));
        }
    }
}
