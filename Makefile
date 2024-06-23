.PHONY: help watch-actions release-action changelog-action version version-as release release-dry release-init release-prerelease release-as release-minor release-patch release-major

# Variables
REF := $(if $(ref),$(ref),"dev")
VERSION := $(if $(version),$(version),"")

# Targets for running workflow commands
watch-actions: ## Watch a run until it completes, showing its progress
	gh run watch; notify-send "run is done!"

changelog-action: ## Run changelog action
	gh workflow run Changelog --ref $(REF) -f version=$(VERSION)

# Targets for running standard-version commands
version: ## Get current program version
	node -p -e "require('./package.json').version"

help: ## Display this help message
	@echo "Usage: make <target>"
	@echo ""
	@echo "Targets:"
	@awk -F ':|##' '/^[^\t].+?:.*?##/ { printf "  %-20s %s\n", $$1, $$NF }' $(MAKEFILE_LIST) | sort

.DEFAULT_GOAL := help
