using System.Collections.Generic;
using CTOnline.OpenServicesFx;

namespace SampleServicesLibrary
{
    public interface IValuesService
    {
        ServiceResponse<IEnumerable<Value>> GetAllItems();
        ServiceResponse<Value> GetItemById(int id);
        ServiceResponse<Value> CreateItem(string value);
        ServiceResponse<bool> UpdateItem(int id, string value);
        ServiceResponse<bool> RemoveItemById(int id);
    }
}
