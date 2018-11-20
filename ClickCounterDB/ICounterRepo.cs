using System;
using System.Collections.Generic;
using System.Text;

namespace ClickCounterDB
{
    public interface ICounterRepo
    {
        void Count(string hash);
        List<Fingerprint> GetFingerprints();
    }
}
