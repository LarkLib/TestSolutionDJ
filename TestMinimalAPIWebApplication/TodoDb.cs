﻿using Microsoft.EntityFrameworkCore;

namespace TestMinimalAPIWebApplication
{

    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
