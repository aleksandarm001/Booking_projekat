using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    /*
    public class Repository<T> 
    {
        private readonly string _filePath;
        private readonly Serializer<T> _serializer;
        private List<T> _entities;

        public Repository(string filePath)
        {
            _filePath = filePath;
            _serializer = new Serializer<T>();
            _entities = new List<T>();
        }

        public List<T> GetAll()
        {
            return _serializer.FromCSV(_filePath);
        }

        public T Save(T entity)
        {
            entity.GetType().GetProperty(GetEntityIdName()).SetValue(entity, NextId());
            _entities.Add(entity);
            _serializer.ToCSV(_filePath, _entities);
            return entity;
        }

        public void Delete(T entity)
        {
            _entities = _serializer.FromCSV(_filePath);
            T foundedEntity = _entities.Find(e => GetEntityId(e).Equals(GetEntityId(entity)));
            _entities.Remove(foundedEntity);
            _serializer.ToCSV(_filePath, _entities);
        }

        public T Update(T entity)
        {
            _entities = _serializer.FromCSV(_filePath);
            T current = _entities.Find(e => GetEntityId(e).Equals(GetEntityId(entity)));
            int index = _entities.IndexOf(current);
            _entities.Remove(current);
            _entities.Insert(index, entity);
            _serializer.ToCSV(_filePath, _entities);
            return entity;
        }

        private int NextId()
        {
            _entities = _serializer.FromCSV(_filePath);
            int nextId = 1;
            if (_entities.Count > 0)
            {
                nextId = (int)GetEntityId(_entities.Max());
                nextId++;
            }
            return nextId;
        }

        private string GetEntityIdName()
        {
            return typeof(T).Name + "ID";
        }

        private object GetEntityId(T entity)
        {
            return entity.GetType().GetProperty(GetEntityIdName()).GetValue(entity);
        }
    }
    */
}
