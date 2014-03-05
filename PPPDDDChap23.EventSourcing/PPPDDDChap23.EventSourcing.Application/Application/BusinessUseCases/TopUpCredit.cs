using System;
using DDDPPP.Chap19.RavenDBExample.Application.Model.Auction;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;
using Raven.Client;
using Raven.Abstractions.Exceptions;
using DDDPPP.Chap19.RavenDBExample.Application.Infrastructure;

namespace DDDPPP.Chap19.RavenDBExample.Application.Application.BusinessUseCases
{
    public class TopUpCredit
    {
        private IOpenOrderRepository _openOrderRepository;
        private IDocumentSession _unitOfWork;

        public BidOnAuction(IOpenOrderRepository openOrderRepository,
                            IDocumentSession unitOfWork)
        {
            _openOrderRepository = openOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(Guid auctionId, Guid memberId, decimal amount)
        {
            try
            {                                
                var order = new OpenOrder(auctionId);

                var bidAmount = new Money(amount);

                auction.PlaceBidFor(new Offer(memberId, bidAmount, _clock.Time()), _clock.Time());
                                
                _unitOfWork.SaveChanges();         
            }
            catch (ConcurrencyException ex)
            {
                _unitOfWork.Advanced.Clear();
                Bid(auctionId, memberId, amount);
            }
        }

        private Action<BidPlaced> BidPlaced()
        {
            return (BidPlaced e) =>
            {               
                var bidEvent = new Bid(e.AuctionId, e.Bidder, e.AmountBid, e.TimeOfBid);
              
                _bidHistory.Add(bidEvent);
            };
        }

        private Action<OutBid> OutBid()
        {
            return (OutBid e) =>
            {
                // Email customer to say that he has been out bid                
            };
        }
    }
}
