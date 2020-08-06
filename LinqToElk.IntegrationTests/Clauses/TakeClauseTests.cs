﻿using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace LinqToElk.IntegrationTests.Clauses
{
    public class TakeClauseTests: IntegrationTestsBase<SampleData>
    {
        [Fact]
        public void TakeObjects()
        {
            //Given
            var datas = Fixture.CreateMany<SampleData>(11);

            Bulk(datas);
            
            ElasticClient.Indices.Refresh();
            
            //When
            var results = Sut.Take(5).ToList();

            //Then
            results.Count().Should().Be(5);
        }
    }
}