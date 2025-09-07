BEGIN TRY
    BEGIN TRANSACTION;

    INSERT INTO Companies (UserId, Name, CNPJ, CreatedAt)
    VALUES ('42a03cac-8dec-4a36-9b4a-7c5d2c03f332', 'Test Company', '12345678000199', GETDATE());

    DECLARE @CompanyId INT = (SELECT TOP 1 CompanyId FROM Companies WHERE UserId = '42a03cac-8dec-4a36-9b4a-7c5d2c03f332');

    INSERT INTO Categories (Name) VALUES ('Receita');
    INSERT INTO Categories (Name) VALUES ('Despesa');

    DECLARE @CategoryReceitaId INT = (SELECT TOP 1 CategoryId FROM Categories WHERE Name = 'Receita');
    DECLARE @CategoryDespesaId INT = (SELECT TOP 1 CategoryId FROM Categories WHERE Name = 'Despesa');

    DECLARE @Month INT = 1;
    WHILE @Month <= 9
    BEGIN
        DECLARE @i INT = 1;
        WHILE @i <= 10
        BEGIN
            DECLARE @Type INT = CASE WHEN @i % 2 = 0 THEN 1 ELSE 2 END; -- 1=Entrada, 2=Saida
            DECLARE @CategoryId INT = CASE WHEN @i % 3 = 0 THEN @CategoryDespesaId ELSE @CategoryReceitaId END;
            DECLARE @Amount DECIMAL(18,2) = 100 + (@Month * 50) + (@i * 10); -- Valor variado e crescente
            DECLARE @Date DATE = DATEFROMPARTS(2025, @Month, CASE WHEN @i < 29 THEN @i ELSE 28 END);

            INSERT INTO FinancialTransactions (
                CompanyId, CategoryId, Type, Description, Amount, Date
            )
            VALUES (
                @CompanyId,
                @CategoryId,
                @Type,
                CONCAT('Transação ', @i, ' de ', DATENAME(MONTH, @Date), ' 2025'),
                @Amount,
                @Date
            );

            SET @i = @i + 1;
        END
        SET @Month = @Month + 1;
    END

    SET @Month = 1;
    WHILE @Month <= 9
    BEGIN
        DECLARE @TargetAmount DECIMAL(18,2) = 5000 + (@Month * 1000); -- Meta crescente por mês
        INSERT INTO MonthlyGoals (CompanyId, Year, Month, TargetAmount)
        VALUES (@CompanyId, 2025, @Month, @TargetAmount);
        SET @Month = @Month + 1;
    END

    DECLARE @AnnualTargetRevenue DECIMAL(18,2) = 70000;
    DECLARE @AnnualTargetProfit DECIMAL(18,2) = 20000;
    INSERT INTO AnnualGoals (CompanyId, Year, TargetRevenue, TargetProfit)
    VALUES (@CompanyId, 2025, @AnnualTargetRevenue, @AnnualTargetProfit);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Erro ao inserir dados de mock: %s', 16, 1, @ErrorMessage);
END CATCH