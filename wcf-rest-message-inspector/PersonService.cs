using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace wcf_rest_message_inspector
{
    [ServiceContract]
    interface IPersonService
    {
        [OperationContract]
        [WebInvoke(
            Method = "*",
            UriTemplate = "Person/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Person GetPerson(string id);

        [OperationContract]
        List<Person> GetPersons();
    }

    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonService : IPersonService
    {
        [WebGet(
            UriTemplate = "Persons",
            ResponseFormat = WebMessageFormat.Xml)]
        public List<Person> GetPersons()
        {
            List<Person> ps = new List<Person>();
            Person p = new Person();
            p.id = 1;
            p.name = "Test 1";
            ps.Add(p);
            p.id = 2;
            p.name = "Test 2";
            ps.Add(p);
            p.id = 3;
            p.name = "Test 3";
            ps.Add(p);
            return ps;
        }

        public Person GetPerson(string id)
        {
            Person p = new Person();
            p.id = 1;
            p.name = "Test";
            return p;
        }
    }
}