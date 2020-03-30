﻿using Template_WebAPI.Interfaces;
using Template_WebAPI.Models;

namespace Template_WebAPI.Repository
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(IMongoContext context) : base(context)
        {

        }
    }
}
