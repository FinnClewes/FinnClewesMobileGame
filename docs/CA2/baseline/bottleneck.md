# What
Placeholder moving squares script with generated grid.
# Where
Canvas.Renderverlays
# Numbers
Main-Thread 30.63ms
SetPass 2 calls (0.02ms)
GC alloc 1 call (0ms)
frame time full 0ms 
frame time half 0ms 
# Verdict
This is CPU-bound but only barely on the threshold as frame time dropped by slighly less than a third
# Fix to try
To make it run better, I would find a better way to have a grid and not have as many moving objects at once.
# Link
docs\CA2\baseline\w03-bad-frame.png
docs\CA2\baseline\w03-gpu-pass.png