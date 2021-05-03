using System;

namespace IPISserver.Handlers
{
    interface ITableHandler<T> where T:class
    {
        string? Select();
        string? Select(int id);
        bool InsertNewRow(GendersModel model);
        bool UpdateRow(GendersModel model);
        bool DeleteRow(int id);
    }
}