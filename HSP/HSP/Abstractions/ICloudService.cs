using System;
using System.Collections.Generic;
using System.Text;

namespace HSP.Abstractions
{
    /*************************************************************************
     We will rely on interfaces for defining the shape for the class
     for any service that we interact with. This is really not important
     in small projects like this one. This technique allows us to mock the
     backend service, as we shall see later on. Mocking the backend service 
     is a great technique to rapidly iterate on the front end mobile client
     without getting tied into what the backend is doing.
     this is defined in Abstractions\ICloudService.cs. 
     It is used for initializing the connection and getting a table definition.
     ****************************************************************************/

    public interface ICloudService
    {
        ICloudTable<T> GetTable<T> () where T : TableData;

    }
}
