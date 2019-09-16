using System;
using System.Collections.Generic;

namespace Ghis.GuardLib
{
    /// <summary>
    /// REF : https://github.com/ardalis/GuardClauses/blob/master/src/GuardClauses/GuardClauseExtensions.cs
    /// </summary>
    public interface IGuard
    {
        void Default<T>(T input, string parameterName);

        void Null(object input, string parameterName);

        void NullOrEmpty(string input, string parameterName);

        void NullOrEmpty<T>(IEnumerable<T> input, string parameterName);

        void NullOrWhiteSpace(string input, string parameterName);

        void OutOfRange(int input, string parameterName, int rangeFrom, int rangeTo);

        void OutOfRange(DateTime input, string parameterName, DateTime rangeFrom, DateTime rangeTo);

        void OutOfRange<T>(int input, string parameterName) where T : Enum;

        void Zero<T>(T input, string parameterName);
    }

    public class Guard : IGuard
    {
        public void Default<T>(T input, string parameterName)
        {
            throw new NotImplementedException();
        }

        public void Null(object input, string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public void NullOrEmpty(string input, string parameterName)
        {
            throw new NotImplementedException();
        }

        public void NullOrEmpty<T>(IEnumerable<T> input, string parameterName)
        {
            throw new NotImplementedException();
        }

        public void NullOrWhiteSpace(string input, string parameterName)
        {
            throw new NotImplementedException();
        }

        public void OutOfRange(int input, string parameterName, int rangeFrom, int rangeTo)
        {
            throw new NotImplementedException();
        }

        public void OutOfRange(DateTime input, string parameterName, DateTime rangeFrom, DateTime rangeTo)
        {
            throw new NotImplementedException();
        }

        void IGuard.OutOfRange<T>(int input, string parameterName)
        {
            throw new NotImplementedException();
        }

        public void Zero<T>(T input, string parameterName)
        {
            throw new NotImplementedException();
        }
    }
}