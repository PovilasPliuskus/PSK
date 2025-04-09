import React from 'react';
import Form from 'react-bootstrap/Form';
import { FormControlProps, FormCheckProps } from 'react-bootstrap';

interface BaseInputProps {
  label?: string;
  id?: string;
  className?: string;
  error?: string;
  required?: boolean;
  editing? : boolean;
}

// Text Input Component
interface TextInputProps extends BaseInputProps, FormControlProps {
  placeholder?: string;
}

export const TextInput: React.FC<TextInputProps> = ({
  label,
  id,
  className,
  error,
  required = false,
  ...props
}) => {
  const inputId = id || `text-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3" controlId={inputId}>
      {label && (
        <Form.Label>
          {label} {required && <span className="text-danger">*</span>}
        </Form.Label>
      )}
      <Form.Control
        type="text"
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      />
      <Form.Control.Feedback type="invalid">
        {error}
      </Form.Control.Feedback>
    </Form.Group>
  );
};

// Textarea Input Component
interface TextareaInputProps extends BaseInputProps, FormControlProps {
  placeholder?: string;
}

export const TextareaInput: React.FC<TextareaInputProps> = ({
  label,
  id,
  className,
  error,
  required = false,
  ...props
}) => {
  const inputId = id || `textarea-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3" controlId={inputId}>
      {label && (
        <Form.Label>
          {label} {required && <span className="text-danger">*</span>}
        </Form.Label>
      )}
      <Form.Control
        as="textarea"
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      />
      <Form.Control.Feedback type="invalid">
        {error}
      </Form.Control.Feedback>
    </Form.Group>
  );
};

// Select Input Component
interface SelectInputProps extends BaseInputProps {
  options: Array<{ value: string; label: string }>;
  placeholder?: string;
}

export const SelectInput: React.FC<SelectInputProps> = ({
  label,
  id,
  options,
  className,
  error,
  required = false,
  placeholder,
  ...props
}) => {
  const inputId = id || `select-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3" controlId={inputId}>
      {label && (
        <Form.Label>
          {label} {required && <span className="text-danger">*</span>}
        </Form.Label>
      )}
      <Form.Select
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      >
        {placeholder && <option value="">{placeholder}</option>}
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </Form.Select>
      <Form.Control.Feedback type="invalid">
        {error}
      </Form.Control.Feedback>
    </Form.Group>
  );
};

// Number Input Component
interface NumberInputProps extends BaseInputProps, FormControlProps {
  min?: number;
  max?: number;
}

export const NumberInput: React.FC<NumberInputProps> = ({
  label,
  id,
  className,
  error,
  required = false,
  min,
  max,
  ...props
}) => {
  const inputId = id || `number-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3" controlId={inputId}>
      {label && (
        <Form.Label>
          {label} {required && <span className="text-danger">*</span>}
        </Form.Label>
      )}
      <Form.Control
        type="number"
        min={min}
        max={max}
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      />
      <Form.Control.Feedback type="invalid">
        {error}
      </Form.Control.Feedback>
    </Form.Group>
  );
};

// Checkbox Input Component
interface CheckboxInputProps extends BaseInputProps, FormCheckProps {
  label: string;
}

export const CheckboxInput: React.FC<CheckboxInputProps> = ({
  id,
  label,
  className,
  error,
  ...props
}) => {
  const inputId = id || `checkbox-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3">
      <Form.Check
        id={inputId}
        type="checkbox"
        label={label}
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      />
      {error && (
        <Form.Text className="text-danger">
          {error}
        </Form.Text>
      )}
    </Form.Group>
  );
};

// Date Input Component
interface DateInputProps extends BaseInputProps, FormControlProps {
  min?: string;
  max?: string;
}

export const DateInput: React.FC<DateInputProps> = ({
  label,
  id,
  className,
  error,
  required = false,
  min,
  max,
  ...props
}) => {
  const inputId = id || `date-input-${Math.random().toString(36).substr(2, 9)}`;
  
  return (
    <Form.Group className="mb-3" controlId={inputId}>
      {label && (
        <Form.Label>
          {label} {required && <span className="text-danger">*</span>}
        </Form.Label>
      )}
      <Form.Control
        type="date"
        min={min}
        max={max}
        isInvalid={!!error}
        className={className}
        disabled={!props.editing}
        {...props}
      />
      <Form.Control.Feedback type="invalid">
        {error}
      </Form.Control.Feedback>
    </Form.Group>
  );
};