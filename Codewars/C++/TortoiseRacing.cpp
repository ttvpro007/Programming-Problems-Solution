#include "TortoiseRacing.h"

std::vector<int> TortoiseRacing::race(int v1, int v2, int x10)
{
	return v2 > v1 ? std::vector<int> { -1, -1, -1 } : std::vector<int>{ x10 / (v2 - v1), (x10 * 60 / (v2 - v1)) % 60, (x10 * 3600 / (v2 - v1)) % 60 };
}