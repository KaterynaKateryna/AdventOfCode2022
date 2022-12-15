using AdventOfCode2022.Day15;

BeaconExclusionZone beaconExclusionZone = new BeaconExclusionZone();
Sensor[] sensors = await beaconExclusionZone.GetInput();

// part 1
long points = beaconExclusionZone.CantContainBeacon(sensors, 2000000);
Console.WriteLine(points);

// part 2
long frequency = beaconExclusionZone.GetTuningFrequency(sensors, 0, 4000000);
Console.WriteLine(frequency);
