using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCCRD.Services.DataV2.Extensions
{
    public static class HelperExtensions
    {
        public static void ClearIdentityValue<T>(ref T model)
        {
            var identityProp = model.
                        GetType().
                        GetProperties().
                        FirstOrDefault(x => x.Name == "Id" || x.Name == (typeof(T).Name + "Id"));

            if(identityProp != null)
            {
                identityProp.SetValue(model, 0);
            }
        }

        public static void ClearNullableInts<T>(ref T model)
        {
            var nullableIntProps = model.
                                    GetType().
                                    GetProperties().
                                    Where(x => x.PropertyType == typeof(int?));

            foreach (var prop in nullableIntProps)
            {
                var propVal = prop.GetValue(model);
                if (propVal != null)
                {
                    if (int.TryParse(propVal.ToString(), out int test) && test == 0)
                    {
                        prop.SetValue(model, null);
                    }
                }
            }
        }
    }
}
