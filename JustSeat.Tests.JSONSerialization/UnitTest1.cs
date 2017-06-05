using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JustSeat.Tests.JSONSerialization
{
    [TestClass]
    public class TestSerialization
    {
        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var serializer = new JustSeat.JSONSerialization.JustSeatJSONSerializer();

            var model = new JustSeat.Serialization.JustSeat
            {
                ZoomLevel = 2,
                Event = new Serialization.Event
                {
                    NotSeatedGuets = new System.Collections.Generic.List<Model.Guest>()
                     {
                           new Model.Guest(){ GuestType = Model.GuestType.YoungCouple, Name = "Geralt", Surname = "Z Rivii"},
                           new Model.Guest() { GuestType = Model.GuestType.GroomFamily, Name = "Zdzichu", Surname = "Pyra"}
                     },

                    Tables = new System.Collections.Generic.List<Model.Table>()
                     {
                         new Model.Table
                         {
                             BottomChairs = new System.Collections.ObjectModel.ObservableCollection<Model.Chair>
                             {
                              new Model.Chair {Person = new Model.Guest{Name = "Jacek", Surname = "Soplica", GuestType = Model.GuestType.GroomsMen } }
                              },

                              LeftChairs = new System.Collections.ObjectModel.ObservableCollection<Model.Chair>{ new Model.Chair { }, new Model.Chair { } }
                         },

                         new Model.Table{ }
                     }
                }
            };

            var exported = serializer.Export(model);

            var imported = serializer.Import(exported);

        }
    }
}
