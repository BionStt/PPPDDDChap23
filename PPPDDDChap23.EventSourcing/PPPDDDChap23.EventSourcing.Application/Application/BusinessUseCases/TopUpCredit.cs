﻿using System;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;
using Raven.Client;
using Raven.Abstractions.Exceptions;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Application.BusinessUseCases
{
    public class TopUpCredit
    {
        private IPayAsYouGoAccountRepository _payAsYouGoAccountRepository;
        private IDocumentSession _unitOfWork;

        public TopUpCredit(IPayAsYouGoAccountRepository payAsYouGoAccountRepository,
                           IDocumentSession unitOfWork)
        {
            _payAsYouGoAccountRepository = payAsYouGoAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public void Execute(Guid id, decimal amount)
        {
            try
            {
                var account = _payAsYouGoAccountRepository.FindBy(id);

                var credit = new Money(amount);

                account.TopUp(credit); 

                _payAsYouGoAccountRepository.Save(account);
                                               
                _unitOfWork.SaveChanges();         
            }
            catch (ConcurrencyException ex)
            {
                _unitOfWork.Advanced.Clear();

                // TODO: Add logic to retry X times then move to an error queue
                // Execute(id, amount);

                throw ex;
            }
        }
    }
}
