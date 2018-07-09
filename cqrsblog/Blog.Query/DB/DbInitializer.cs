using System;
using System.Collections.Generic;

namespace Blog.Query.DB
{
    public static class DbInitializer
    {
        public static InMemoryQueryDB Init()
        {
            var db = new InMemoryQueryDB();

            return db;
        }
    }
}