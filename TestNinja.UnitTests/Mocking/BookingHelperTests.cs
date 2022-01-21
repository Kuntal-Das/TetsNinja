using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class BookingHelperTests
{
    private Mock<IBookingRepo> _mockBookingRepo;
    private Booking _existingBooking;

    [SetUp]
    public void SetUp()
    {
        _mockBookingRepo = new Mock<IBookingRepo>();
        _existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2022, 1, 1),
            DepartureDate = DepartOn(2022, 1, 5),
            Reference = "a"
        };
        _mockBookingRepo
            .Setup(bk => bk.GetActiveBookings(1))
            .Returns(new List<Booking>() {_existingBooking}.AsQueryable());
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper
            .OverlappingBookingsExist(
                new Booking()
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate),
                    Status = string.Empty
                }
                , _mockBookingRepo.Object
            );

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void
        OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
    {
        var result = BookingHelper
            .OverlappingBookingsExist(
                new Booking()
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate),
                    Status = string.Empty
                }
                , _mockBookingRepo.Object
            );

        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    private DateTime Before(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(-days);
    }

    private DateTime After(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }

    private DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }

    private DateTime DepartOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 10, 0, 0);
    }
}