resource "aws_dynamodb_table" "finance_summary_table" {
    name                  = "FinancialSummaries"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "id"

    attribute {
        name              = "id"
        type              = "S"
    }

    tags = {
        Name              = "financial-summary-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
    }

    point_in_time_recovery {
        enabled           = true
    }
}

resource "aws_dynamodb_table" "transaction_summary_table" {
    name                  = "TransactionSummaries"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "id"

    attribute {
        name              = "id"
        type              = "S"
    }

    tags = {
        Name              = "financial-summary-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
    }

    point_in_time_recovery {
        enabled           = true
    }
}

