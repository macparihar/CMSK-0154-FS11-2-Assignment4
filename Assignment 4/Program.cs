using System;
using System.Collections.Generic;

public enum SeatLabel { A, B, C, D }
public enum SeatPreference { Window, Aisle }

public class Passenger
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public SeatPreference SeatPreference { get; set; }
    public Seat BookedSeat { get; set; }
}

public class Seat
{
    public SeatLabel Label { get; set; }
    public bool IsBooked { get; set; }
    public Passenger Passenger { get; set; }
    public int Row { get; set; }
}

public class Row
{
    public int RowNumber { get; set; }
    public List<Seat> Seats { get; set; }
}

public class Plane
{
    public List<Row> Rows { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        
        Plane plane = new Plane();
        plane.Rows = new List<Row>();
        for (int i = 1; i <= 12; i++)
        {
            Row row = new Row();
            row.RowNumber = i;
            row.Seats = new List<Seat>();
            foreach (SeatLabel label in Enum.GetValues(typeof(SeatLabel)))
            {
                Seat seat = new Seat();
                seat.Label = label;
                seat.Row = i;
                row.Seats.Add(seat);
            }
            plane.Rows.Add(row);
        }

        
        while (true)
        {
            Console.WriteLine("Please enter 1 to book a ticket.");
            Console.WriteLine("Please enter 2 to see seating chart.");
            Console.WriteLine("Please enter 3 to exit the application.");
            int option = Convert.ToInt32(Console.ReadLine());

            if (option == 1)
            {
                // Book a ticket
                Console.WriteLine("Please enter the passenger's first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Please enter the passenger's last name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick first available seat.");
                int seatPreference = Convert.ToInt32(Console.ReadLine());

               
                Passenger passenger = new Passenger();
                passenger.FirstName = firstName;
                passenger.LastName = lastName;
                passenger.SeatPreference = (SeatPreference)seatPreference;

                
                foreach (Row row in plane.Rows)
                {
                    foreach (Seat seat in row.Seats)
                    {
                        if (!seat.IsBooked && (seat.Label == SeatLabel.A || seat.Label == SeatLabel.D) == (passenger.SeatPreference == SeatPreference.Window))
                        {
                            
                            seat.IsBooked = true;
                            seat.Passenger = passenger;
                            passenger.BookedSeat = seat;
                            Console.WriteLine($"The seat located in {seat.Row} {seat.Label} has been booked.");
                            break;
                        }
                    }
                    if (passenger.BookedSeat != null)
                    {
                        break;
                    }
                }

                if (passenger.BookedSeat == null)
                {
                    Console.WriteLine("Sorry, the plane is completely booked.");
                }
            }
            else if (option == 2)
            {
               
                foreach (Row row in plane.Rows)
                {
                    foreach (Seat seat in row.Seats)
                    {
                        if (seat.IsBooked)
                        {
                            Console.Write($"{seat.Passenger.FirstName[0]}{seat.Passenger.LastName[0]} ");
                        }
                        else
                        {
                            Console.Write($"{seat.Label} ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            else if (option == 3)
            {
               
                break;
            }
        }
    }
}
