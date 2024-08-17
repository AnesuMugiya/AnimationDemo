def count_ways(n):
    # Initialize base cases
    if n == 0:
        return 1
    elif n == 1:
        return 1
    elif n == 2:
        return 1  # Only one way: 1+1
    elif n == 3:
        return 2  # Two ways: 1+1+1, 3
     #test
    # Initialize an array to store the number of ways to reach each step
    ways = [0] * (n + 1)
    ways[0] = 1
    ways[1] = 1
    ways[2] = 1
    ways[3] = 2
    
    # Fill the array using the recursive relation
    for i in range(4, n + 1):
        ways[i] = ways[i - 1] + ways[i - 3]
    
    return ways[n]

# Calculate the number of ways to climb 44 steps
result = count_ways(44)
print("Number of distinct ways to climb 44 steps:", result)
      