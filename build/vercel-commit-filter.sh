#!/bin/bash

echo "Branch: $VERCEL_GIT_COMMIT_REF"

if [[ "$VERCEL_GIT_COMMIT_REF" != "gh-pages" ]] ; then
  # Proceed with the build
  echo "✅ - Build can proceed"
  exit 1;

else
  # Don't build
  echo "🛑 - Skipped build, branch is ignored"
  exit 0;
fi