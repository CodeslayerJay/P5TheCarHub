﻿using Microsoft.EntityFrameworkCore;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Vehicle GetByVin(string vin, bool withIncludes = false)
        {

            if(!withIncludes)
                return _context.Vehicles.Where(x => x.VIN.ToUpper() == vin.ToUpper()).SingleOrDefault();

            return _context.Vehicles.Where(x => x.VIN.ToUpper() == vin.ToUpper())
                .Include(x => x.Repairs)
                .Include(x => x.Photos)
                .Include(x => x.Invoice)
                .SingleOrDefault();
        }

        public Vehicle Add(Vehicle entity)
        {            
            _context.Vehicles.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(x => x.Id == id);

            if (vehicle != null)
                _context.Vehicles.Remove(vehicle);
        }


        public IEnumerable<Vehicle> GetAll(int? amount = null)
        {
            var filter = new VehicleFilter { Size = amount };

            return GetAll(filter).ToList();
        }

        public IEnumerable<Vehicle> GetAll(VehicleFilter filter)
        {
            return Query(filter).ToList();
        }


        private IQueryable<Vehicle> Query(VehicleFilter filter, Expression<Func<Vehicle, bool>> predicate = null)
        {
            var query = _context.Vehicles.Where(x => x.Id > 0);

            if (predicate != null)
                query = query.Where(predicate);

            if (filter.IncludeInvoice)
                query = query.Include(x => x.Invoice);

            if (filter.IncludePhotos)
                query = query.Include(x => x.Photos);

            if (filter.IncludeRepairs)
                query = query.Include(x => x.Repairs);

            if (filter.VehicleStatus.HasValue)
            {
                if(filter.VehicleStatus.Value >= 0)
                {
                    query = query.Where(x => (int)x.AvailableStatus == filter.VehicleStatus.Value);
                }
            }
                

            if (filter.MinPrice.HasValue)
            {
                if(filter.MinPrice.Value >= 0)
                {
                    query = query.Where(x => x.SalePrice >= filter.MinPrice.Value);
                }
            }


            if (filter.MaxPrice.HasValue)
            {
                if(filter.MaxPrice.Value >= 0)
                {
                    query = query.Where(x => x.SalePrice <= filter.MaxPrice.Value);
                }
            }
                

            if (filter.Skip.HasValue)
            {
                if(filter.Size.Value >= 0)
                {
                    query = query.Skip(filter.Skip.Value);
                }
            }
                

            if (filter.Size.HasValue)
            {
                if(filter.Size.Value >= 0)
                {
                    query = query.Take(filter.Size.Value);
                }
            }
                

            query = query.OrderBy(x => x.SalePrice);

            //query = (filter.OrderByDescending) ? query.OrderByDescending(x => x.LotDate) : query.OrderBy(x => x.LotDate);
            
            return query;
        }


        public Vehicle GetById(int id)
        {
            return _context.Vehicles.SingleOrDefault(x => x.Id == id);
        }

        public Vehicle GetById(int id, bool withIncludes)
        {
            if (!withIncludes)
                return _context.Vehicles.Where(x => x.Id == id).SingleOrDefault();

            return _context.Vehicles.Where(x => x.Id == id)
                .Include(x => x.Repairs)
                .Include(x => x.Photos)
                .Include(x => x.Invoice)
                .SingleOrDefault();
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate)
        {
            var query = GetAll().AsQueryable();
            query = query.Where(predicate);
            return query.ToList();
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate, VehicleFilter filter = null)
        {
            return Query(filter, predicate).ToList();
        }

    }
}

