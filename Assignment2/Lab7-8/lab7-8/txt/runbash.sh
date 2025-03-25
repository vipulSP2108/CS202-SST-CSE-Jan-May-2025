# in bash ./runbash.sh
# permission denied chmod +x runbash.sh

commit_hashes=$(git log --no-merges --oneline -n 100 | awk '{print $1}')
# commit_hashes=$(git log -n 100 --format="%H")
echo "Commit Hashes:"
echo "$commit_hashes"
echo ""

counter=1

for commit in $commit_hashes; do
  echo "Checking commit $commit..."
  git checkout $commit

  # bandit -r . --exclude .git -v > "text${counter}.txt"
  bandit -r . > "text${counter}.txt"

  if [ -s "text${counter}.txt" ]; then
    echo "Bandit found issues, results saved to text${counter}.txt"
  else
    echo "No issues found for commit $commit."
  fi
  counter=$((counter + 1))
done

git checkout main