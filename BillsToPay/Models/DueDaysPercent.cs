using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsToPay.Models
{
    public class DueDaysPercent
    {
        public static readonly DueDaysPercent NoDue = new DueDaysPercent((int delayedDays) => delayedDays <= 0, 0, 0);
        public static readonly DueDaysPercent Till3Days = new DueDaysPercent((int delayedDays) => delayedDays > 0 && delayedDays <= 3, 0.02m, 0.001m);
        public static readonly DueDaysPercent Between3And5Days = new DueDaysPercent((int delayedDays) => delayedDays > 3 && delayedDays <= 5, 0.03m, 0.002m);
        public static readonly DueDaysPercent After5Days = new DueDaysPercent((int delayedDays) => delayedDays > 5, 0.05m, 0.003m);

        public static IEnumerable<DueDaysPercent> Values
        {
            get
            {
                yield return NoDue;
                yield return Till3Days;
                yield return Between3And5Days;
                yield return After5Days;
            }
        }

        public decimal Fine { get; private set; }
        public decimal Interest { get; private set; }

        private Func<int, bool> validation;
        private DueDaysPercent(Func<int, bool> validation, decimal fine, decimal interest)
        {
            this.validation = validation;
            this.Fine = fine;
            this.Interest = interest;
        }

        public bool Validate(int deleayedDays) => validation(deleayedDays);

    }
}