Feature: SNS is used as our notification service
  In order to improve security
  As engineers
  We'll use ensure our SNS topics are configured correctly

  Scenario: Ensure encryption is enabled
    Given I have aws_sns_topic defined
    Then it must contain kms_master_key_id