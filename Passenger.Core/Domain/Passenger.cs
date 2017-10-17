using System;

namespace Passenger.Core.Domain
{

    public class Passenger
    {
        public Guid Id { get; protected set; }
        public Guid UserID { get; protected set; }
        public Node Address { get; protected set; }
    }
}