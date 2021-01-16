#include "TheSupermarketQueue.h"

long TheSupermarketQueue::queueTime(std::vector<int> customers, int n)
{
    int* lanes = new int[n] {0};
    for (auto i = customers.begin(); i != customers.end(); i++)
    {
        *std::min_element(lanes, lanes + n) += *i;
    }
    return *std::max_element(lanes, lanes + n);
}
