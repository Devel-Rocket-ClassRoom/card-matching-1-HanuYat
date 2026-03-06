using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
Console.WriteLine();

GamePlay cardMatching = new GamePlay(4, 4);
cardMatching.StartGame();