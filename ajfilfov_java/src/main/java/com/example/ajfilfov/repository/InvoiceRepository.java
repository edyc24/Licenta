package com.example.ajfilfov.repository;

import com.example.ajfilfov.entity.Invoice;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.UUID;

public interface InvoiceRepository extends JpaRepository<Invoice, UUID> { }
