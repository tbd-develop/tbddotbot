// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using twitchstreambot.benchmarks;

BenchmarkRunner.Run<ParsingBenchmarks>();