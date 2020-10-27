using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsToPay.Models
{
    public class BillQuery
    {
        public static readonly string GetAll = @"SELECT b.BILL_NAME AS ""NAME"", 
                                                        b.ORIGINAL_PRICE as ORIGINALPRICE,
                                                        b.DUE_DATE AS DUEDATE, 
                                                        b.INTEREST AS INTEREST, 
                                                        b.FINE AS FINE 
                                                   FROM BILL b;";

        public static readonly string Add = @"INSERT INTO BILL(BILL_NAME, ORIGINAL_PRICE, 
                                                               DUE_DATE, PAY_DATE, 
                                                               INTEREST, FINE) 
                                                       VALUES (@name, @originalprice, 
                                                               to_date(@duedate, 'DD/MM/YYYY'), to_date(@paydate,'DD/MM/YYYY'), 
                                                               @interest, @fine);";
    }
}