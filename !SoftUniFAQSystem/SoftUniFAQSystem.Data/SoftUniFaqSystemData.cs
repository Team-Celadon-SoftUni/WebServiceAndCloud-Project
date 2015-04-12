﻿namespace SoftUniFAQSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Models;
    using Repositories;

    public class SoftUniFaqSystemData : ISoftUniFAQSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public SoftUniFaqSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return (UsersRepository)this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Question> Questions
        {
            get { return (QuestionsRepository)this.GetRepository<Question>(); }
        }

        public IRepository<Answer> Answers
        {
            get { return (AnswersRepository)this.GetRepository<Answer>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepo = typeof(GenericRepository<T>);
                if (type.IsAssignableFrom(typeof(ApplicationUser)))
                {
                    typeOfRepo = typeof(UsersRepository);
                }
                else if (type.IsAssignableFrom(typeof(Question)))
                {
                    typeOfRepo = typeof(QuestionsRepository);
                }
                else if (type.IsAssignableFrom(typeof(Answer)))
                {
                    typeOfRepo = typeof(AnswersRepository);
                }

                var repo = Activator.CreateInstance(typeOfRepo, this.context);
                this.repositories.Add(type, repo);
            }

            return (GenericRepository<T>)this.repositories[type];
        }
    }
}