﻿using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ChangeReservationRequestService : IChangeReservationRequestService
    {
        private readonly ChangeReservationRequestRepository _requestRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly ReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;
        
        public ChangeReservationRequestService()
        {
            _requestRepository = new ChangeReservationRequestRepository();
            _reservationRepository = new ReservationRepository();   
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            
        }

        public List<ChangeReservationRequest> GetRequests(int userId)
        {
            UpdateRequests(userId);
            return _requestRepository.GetAll().Where(r => r.UserId == userId).ToList();
        }

        

        public void SaveRequest(ChangeReservationRequest request)
        {
            if(_requestRepository.GetAll().Find(r => r.ReservationId == request.ReservationId && r.UserId == request.UserId) != null)
            {
                _requestRepository.Update(request);
            }
            else
            {
                _requestRepository.Save(request);
            }
        }

        public void DeleteRequestByReservationId(int reservationId)
        {
            ChangeReservationRequest foundedRequest = _requestRepository.GetAll().Find(r => r.ReservationId == reservationId);
            if (foundedRequest != null)
            {
                _requestRepository.Delete(foundedRequest);
            }
        }
        public ChangeReservationRequest FindRequestByReservationId(int reservationId)
        {
            return _requestRepository.GetAll().First(x => x.ReservationId == reservationId);
        }

        private void UpdateRequests(int userId)
        {
            foreach(Reservation reservation in _reservationService.GetReservationsByUserId(userId))
            {
                if(reservation.ReservationDateRange.StartDate <= DateTime.Now)
                {
                    DeleteRequestByReservationId(reservation.ReservationId);
                }
            }
        }

        public ChangeReservationRequest GetRequestByRequestId(int requestId)
        {
            return _requestRepository.GetAll().Find(r =>r.RequestId == requestId);
        }

        public List<ChangeReservationRequest> GetRequestsByOwnerId(int userId)
        {
            //UpdateRequests(userId);
            return _requestRepository.GetAll().Where(r => r.OwnerId == userId).ToList();
        }


        public List<OwnerChangeRequests> OwnerChangeReservationRequest(int ownerId)
        {
            List<ChangeReservationRequest> AllRequests= GetRequestsByOwnerId(ownerId);
            List<ChangeReservationRequest> changeReservationRequests = AllRequests.Where(r => r.RequestStatus == StatusType.Pending).ToList();

            List<OwnerChangeRequests> ownerChangeRequests = new List<OwnerChangeRequests>();
            

            foreach(ChangeReservationRequest crr in changeReservationRequests)
            {
                OwnerChangeRequests ownerChangeReservationRequests = new OwnerChangeRequests();
                ownerChangeReservationRequests.ReservationId = crr.ReservationId;
                ownerChangeReservationRequests.RequestId= crr.RequestId;
                ownerChangeReservationRequests.AccommodationName = crr.AccommodationName;
                ownerChangeReservationRequests.ReservationId= crr.ReservationId;
                ownerChangeReservationRequests.NewStartDate = crr.NewStartDate;
                ownerChangeReservationRequests.NewEndDate = crr.NewEndDate;
                ownerChangeReservationRequests.OwnerId= crr.OwnerId;
                ownerChangeReservationRequests.IsDateAvailable = isDateRangeAvailable(crr.NewStartDate, crr.NewEndDate, crr.AccommodationName,crr.ReservationId);

                ownerChangeRequests.Add(ownerChangeReservationRequests);
            }
            return ownerChangeRequests;
        }


        public bool isDateRangeAvailable(DateTime newStartDate, DateTime newEndDate,string accommodationName,int reservationId) 
        {

            int accommodationId = _accommodationService.GetAccommodationIdByAccommodationName(accommodationName);
            List<int> reservationsIds = _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodationId);

            List<Reservation> reservations = new List<Reservation>();
            foreach(int id in reservationsIds)
            {
                reservations.Add(_reservationService.GetActiveReservation(id));
            }
           // if (reservations == null)
            //{
              //  return true;
            //}

            foreach (Reservation reservation in reservations)
            {
                if(reservation.ReservationId != reservationId)
                {
                    if (DoRangesIntersect(newStartDate, newEndDate, reservation.ReservationDateRange.StartDate, reservation.ReservationDateRange.EndDate))
                        return false;
                }
            }
            return true;
        }

        public static bool DoRangesIntersect(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if ((start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2)
                || (start2 >= start1 && start2 <= end1) || (end2 >= start1 && end2 <= end1))
            {
                return true; // If ranges intersect, return true
            }
            else
            {
                return false; // If ranges do not intersect, return false
            }
        }



        public void AcceptRequest(int requestId)
        {
            List<ChangeReservationRequest>requests =_requestRepository.GetAll();
            foreach(ChangeReservationRequest r in requests)
            {
                if(r.RequestId == requestId)
                {
                    r.RequestStatus = StatusType.Approved;
                    _requestRepository.Update(r);
                }
            }

        }

        public void ChangeReservationDateRange(DateTime newStartDate , DateTime newEndDate, int reservationId)
        {
            Reservation reservation = _reservationService.GetActiveReservation(reservationId);
            reservation.ReservationDateRange.StartDate = newStartDate;
            reservation.ReservationDateRange.EndDate = newEndDate;
            _reservationRepository.Update(reservation);
            

        }

        public void DeclineRequest(int requestId,string comment)
        {
            ChangeReservationRequest request = GetRequestByRequestId(requestId);
            request.RequestStatus = StatusType.Canceled;
            request.OwnerComment = comment;
            _requestRepository.Update(request);
        }

    }
}
