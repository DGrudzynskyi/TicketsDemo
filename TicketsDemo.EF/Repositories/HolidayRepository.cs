using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Data.Entities.HolidayAggrigate;
using TicketsDemo.Mongo;
using MongoDB.Driver;


namespace TicketsDemo.EF.Repositories
{
    public class HolidayRepository: IHolidayRepository
    {
        public List<Holiday> GetHolidays()
        {
            var ctx = new TContext();
            var holidays = ctx.Holidays.Find(new BsonDocument()).ToList();
            return holidays;
        }

        public Holiday GetInfoAboutHoliday(int id)
        {
            var ctx = new TContext();
            var holiday = ctx.Holidays.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync().Result;
            return holiday;
        }

        public Holiday GetInfoAboutHoliday(DateTime date)
        {
            var ctx = new TContext();
            var holiday = ctx.Holidays.Find(new BsonDocument("Date", date)).FirstOrDefaultAsync().Result;
            return holiday;
        }

        public void CreateHoliday(Holiday holiday)
        {
            var ctx = new TContext();
            ctx.Holidays.InsertOneAsync(holiday);
        }
        public void UpdateHoliday(Holiday holiday)
        {
            var ctx = new TContext();
            ctx.Holidays.ReplaceOne(new BsonDocument("_id", holiday.ID), holiday);
        }
        public void DeleteHoliday(Holiday holiday)
        {
            var ctx = new TContext();
            ctx.Holidays.DeleteOne(new BsonDocument("_id", holiday.ID));
        }
    }
}
