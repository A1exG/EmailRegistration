﻿using System;
using System.Collections.Generic;

namespace EmailRegistration.WebService.Repository
{
    public interface IRepository<T> where T:class
    {
        List<T> Get();
        T GetByID(int id);
        int Insert(T t);
        int Update(T t);
        List<T> GetDateTimePeriod(DateTime start, DateTime end);
        List<T> GetByTo(string str);
        List<T> GetByFrom(string str);
        List<T> GetByTag(string str);
    }
}
