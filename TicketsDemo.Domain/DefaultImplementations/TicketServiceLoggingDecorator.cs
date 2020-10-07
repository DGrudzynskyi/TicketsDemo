using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    abstract class TicketServiceLoggingDecorator : ITicketService
    {
        private ITicketService _decoratedobject;
        string choice = "F";
        string file_path = @"D:\Tikitake true\log.txt";
        public TicketServiceLoggingDecorator(ITicketService service)
        {
            _decoratedobject = service;
        }

        public Ticket CreateTicket(int reservationId, string firstName, string lastName)
        {
            switch (choice)
            {
                case "D":
                    Debug.WriteLine($"attempt to create ticket with {reservationId}, for {firstName}-{lastName}");
                    try
                    {
                        var decorTicket = _decoratedobject.CreateTicket(reservationId, firstName, lastName);
                        Debug.WriteLine($"Ticket created succesfully for {reservationId} with passenger {firstName}-{lastName}");
                        return decorTicket;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.Message} \n Error due to create Ticket for Reservation: {reservationId} with passenger {firstName}-{lastName}");
                        throw ex;
                    }
                case "F":
                    using (FileStream fstream = new FileStream($"{file_path}", FileMode.OpenOrCreate))
                    {
                        string result = "";
                        try
                        {
                            var decorTicket = _decoratedobject.CreateTicket(reservationId, firstName, lastName);
                            result += ($"Ticket created succesfully for {reservationId} with passenger {firstName}-{lastName}");

                            byte[] array = System.Text.Encoding.Default.GetBytes(result);
                            fstream.Write(array, 0, array.Length);
                            return decorTicket;
                        }
                        catch (Exception ex)
                        {
                            result += ($"{ex.Message} \n Error due to create Ticket for Reservation: {reservationId} with passenger {firstName}-{lastName}");
                            byte[] array = System.Text.Encoding.Default.GetBytes(result);
                            fstream.Write(array, 0, array.Length);
                            throw ex;
                        }

                    }
            }
        }

        public void SellTicket(Ticket ticket)
        {
            _decoratedobject.SellTicket(ticket);
        }
    }
}
