def extract_execution_times(file_path):
    execution_times = []
    
    with open(file_path, 'r') as file:
        for line in file:
            if "passed" in line and "in" in line:
                try:
                    time = float(line.split(" in ")[1].split("s")[0].strip())
                    execution_times.append(time)
                except ValueError:
                    continue
                
    return execution_times

def calculate_average_execution_time(execution_times):
    if len(execution_times) == 0:
        return 0
    return sum(execution_times) / len(execution_times)

file_path = 'test_5times.txt'
execution_times = extract_execution_times(file_path)
average_time = calculate_average_execution_time(execution_times)

print(f"Execution times: {execution_times}")
print(f"Average execution time: {average_time}s")
