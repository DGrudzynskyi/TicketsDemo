﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface ICarriageRepository
    {
        Carriage GetCarriage(int number);
    }
}