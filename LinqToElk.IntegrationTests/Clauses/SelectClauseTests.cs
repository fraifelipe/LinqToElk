using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace LinqToElk.IntegrationTests.Clauses
{
    public class SelectClauseTests: IntegrationTestsBase<SampleData>
    {
        public SelectClauseTests()
        {
            Sut = new ElasticQueryable<SampleData>(ElasticClient, DataId);
        }
        
        [Fact]
        public void SelectOneObject()
        {
            //Given
            var data = Fixture.Create<SampleData>();

            Index(data);
            
            ElasticClient.Indices.Refresh();
            
            //When
            var results = from i in Sut select i;
            var listResults = results.ToList();

            //Then
            listResults.Count.Should().Be(1);
            listResults[0].Name.Should().Be(data.Name);
            listResults[0].Description.Should().Be(data.Description);
        }

        [Fact] public void SelectTwoObject()
        {
            //Given
            var datas = Fixture.CreateMany<SampleData>();

            Bulk(datas);
            
            ElasticClient.Indices.Refresh();
            
            //When
            var results = Sut;
            var listResults = results.ToList();

            //Then
            listResults.Count.Should().Be(3);
        }
    }
}