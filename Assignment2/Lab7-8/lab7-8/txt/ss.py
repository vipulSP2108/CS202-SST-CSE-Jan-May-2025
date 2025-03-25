import re
import os
from collections import Counter

# Function to extract CWE codes from the issue report
def extract_cwe_codes(input_string):
    issues = input_string.split('--------------------------------------------------')
    cwe_codes = []

    for issue in issues:
        match = re.search(r'CWE: (CWE-\d+)', issue)
        if match:
            cwe_codes.append(match.group(1))

    return cwe_codes

# Function to count HIGH, MEDIUM, LOW severity and confidence issues
def count_issues_by_severity_and_confidence(input_string):
    severity_counts = {'HIGH': 0, 'MEDIUM': 0, 'LOW': 0}
    confidence_counts = {'HIGH': 0, 'MEDIUM': 0, 'LOW': 0}
    
    # Loop over the issues to count severity and confidence levels
    issues = input_string.split('--------------------------------------------------')
    for issue in issues:
        # Extract severity
        severity_match = re.search(r'Severity: (\w+)', issue)
        if severity_match:
            severity = severity_match.group(1).upper()
            if severity in severity_counts:
                severity_counts[severity] += 1

        # Extract confidence
        confidence_match = re.search(r'Confidence: (\w+)', issue)
        if confidence_match:
            confidence = confidence_match.group(1).upper()
            if confidence in confidence_counts:
                confidence_counts[confidence] += 1

    return severity_counts, confidence_counts

# Function to process multiple files and output the results into a dictionary
def process_files(file_path_pattern, num_files):
    file_info = {}  # Dictionary to store information for each file

    # Loop through the text files
    for i in range(1, num_files + 1):
        file_path = file_path_pattern.format(i)

        if os.path.exists(file_path):
            with open(file_path, 'r') as f:
                input_string = f.read()

            # Extract CWE codes from the current file
            cwe_codes = extract_cwe_codes(input_string)

            # Count issues by severity and confidence
            severity_counts, confidence_counts = count_issues_by_severity_and_confidence(input_string)

            # Get unique CWE codes
            unique_cwe_codes = set(cwe_codes)
            
            # Count occurrences of each CWE code
            cwe_code_counts = dict(Counter(cwe_codes))

            # Store the information for the current file in the dictionary
            file_info[f'file{i}'] = {
                'Total CWE codes': len(cwe_codes),
                'CWE Code Frequency': cwe_code_counts,
                'Severity Counts': severity_counts,
                'Confidence Counts': confidence_counts
            }
        else:
            print(f"File {file_path} does not exist.")

    return file_info

# Define the file pattern and number of files to process
file_path_pattern = "./text{}.txt"  # This pattern should match the actual filenames
num_files = 100  # Change to the actual number of text files you have

# Check and print the current working directory
print("Current working directory:", os.getcwd())

# Process the files and get the dictionary with file information
file_data = process_files(file_path_pattern, num_files)

# Print the resulting dictionary (file_data) for inspection
print(file_data)










import matplotlib.pyplot as plt
import pandas as pd

# Extracting data for plotting
severity_data = []
cwe_code_data = []
confidence_data = []

for file, data in file_data.items():
    # Severity Counts
    severity_data.append(data['Severity Counts'])
    
    # CWE Code Frequency
    cwe_code_data.append(data['CWE Code Frequency'])

    # Confidence Counts
    confidence_data.append(data['Confidence Counts'])

# Convert severity counts and CWE frequencies into DataFrames for easier plotting
severity_df = pd.DataFrame(severity_data, index=file_data.keys())
cwe_code_df = pd.DataFrame(cwe_code_data, index=file_data.keys())
confidence_df = pd.DataFrame(confidence_data, index=file_data.keys())

# Plotting severity counts using a stacked bar chart
ax = severity_df.plot(kind='bar', stacked=True, figsize=(10, 6), title='Severity Counts Across Files', colormap='Set3')
ax.set_ylabel('Count')
ax.set_xlabel('Files')
plt.xticks(rotation=45, ha='right')

# Plotting confidence counts using a stacked bar chart
ax1 = confidence_df.plot(kind='bar', stacked=True, figsize=(10, 6), title='Confidence Counts Across Files', colormap='Set3')
ax1.set_ylabel('Count')
ax1.set_xlabel('Files')
plt.xticks(rotation=45, ha='right')

# Plotting CWE code frequencies using a stacked bar chart
fig, ax2 = plt.subplots(figsize=(10, 6))
cwe_code_df.plot(kind='bar', stacked=True, ax=ax2, colormap='Set2')
ax2.set_ylabel('Count')
ax2.set_xlabel('Files')
ax2.set_title('CWE Code Frequencies Across Files')
plt.xticks(rotation=45, ha='right')

plt.tight_layout()
plt.show()
